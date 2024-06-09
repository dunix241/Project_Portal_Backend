using Application.Core;
using Application.Interfaces;
using Application.Projects.DTOs;
using AutoMapper;
using Domain.EnrollmentPlan;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Projects;

public class ListBasedOnEnrollmentPlan
{
    public class Query : IRequest<Result<ListBasedOnEnrollmentPlanResponseDto>>
    {
        
    }

    public class Handler : IRequestHandler<Query, Result<ListBasedOnEnrollmentPlanResponseDto>>
    {
        private readonly DataContext _dataContext;
        private readonly IUserAccessor _userAccessor;
        private readonly IMapper _mapper;

        public Handler(DataContext dataContext, IUserAccessor userAccessor, IMapper mapper)
        {
            _dataContext = dataContext;
            _userAccessor = userAccessor;
            _mapper = mapper;
        }
        
        public async Task<Result<ListBasedOnEnrollmentPlanResponseDto>> Handle(Query request, CancellationToken cancellationToken)
        {
            var enrollmentPlan = _dataContext.EnrollmentPlans
                .Include(entity => entity.EnrollmentPlanDetailsList)
                .FirstOrDefault(entity => entity.IsActive, null)
                ;
            if (enrollmentPlan == null)
            {
                return Result<ListBasedOnEnrollmentPlanResponseDto>.Success(new ListBasedOnEnrollmentPlanResponseDto());
            }

            var nodes = BuildTree(enrollmentPlan.EnrollmentPlanDetailsList);
            var projectIds = new List<List<Guid>>();
            
            foreach (var node in nodes)
            {
                if (!node.Visited)
                {
                    var root = FindRoot(node);
                    projectIds.Add(Bfs(root));
                }
            }

            ListBasedOnEnrollmentPlanResponseDto result = new ListBasedOnEnrollmentPlanResponseDto();
            result.Projects = projectIds.Select(list =>
            {
                return list.Select(id =>
                {
                    var registrableProject =
                        _mapper.Map<ListBasedOnEnrollmentPlanResponseDto.RegistrableProjectResponseDto>(
                            _dataContext.Projects.FindAsync(id));

                    registrableProject.Enrollment = _dataContext.EnrollmentMembers
                        .Include(entity => entity.Enrollment)
                        .Where(entity =>
                            entity.Email == _userAccessor.GetUser().Email && entity.Enrollment.ProjectId == id &&
                            entity.Enrollment.IsPublished)
                        .FirstOrDefault()!.Enrollment;
                    
                    registrableProject.Registrable =
                        _dataContext.ProjectSemesters.Include(entity => entity.Semester).Any(entity =>
                            entity.ProjectId == id && entity.Semester.StartRegistrationDate <= DateTime.Today &&
                            entity.Semester.EndRegistrationDate >= DateTime.Today)
                        && _dataContext.EnrollmentPlans
                            .Include(entity => entity.EnrollmentPlanDetailsList)
                            .FirstOrDefault(entity => entity.IsActive)
                            .EnrollmentPlanDetailsList
                            .Where(entity => entity.ProjectId == id)
                            .All(entity =>
                                _dataContext.EnrollmentMembers
                                    .Include(entity => entity.Enrollment)
                                    .Where(x =>
                                        x.Email == _userAccessor.GetUser().Email &&
                                        x.Enrollment.ProjectId == entity.PrerequisiteProjectId && x.Enrollment.IsPublished)
                                    .Any()
                            );
                    
                    return registrableProject;
                }).ToList();
            }).ToList();

            return Result<ListBasedOnEnrollmentPlanResponseDto>.Success(result);
        }

        public List<Node?> BuildTree(IList<EnrollmentPlanDetails> enrollmentPlanDetailsList)
        {
            var nodes = new Dictionary<Guid, Node?>();
            
            foreach (var enrollmentPlanDetails in enrollmentPlanDetailsList)
            {
                if (nodes[enrollmentPlanDetails.ProjectId] == null)
                {
                    var node = new Node();
                    node.Id = enrollmentPlanDetails.ProjectId;
                    nodes[node.Id] = node;
                }

                if (nodes[enrollmentPlanDetails.PrerequisiteProjectId] == null)
                {
                    var node = new Node();
                    node.Id = enrollmentPlanDetails.PrerequisiteProjectId;
                    nodes[node.Id] = node;
                }

                var parent = nodes[enrollmentPlanDetails.PrerequisiteProjectId];
                var child = nodes[enrollmentPlanDetails.ProjectId];
                parent.Children.Add(child);
                child.Parents.Add(parent);
            }

            return nodes.Values.ToList();
        }

        public Node FindRoot(Node node)
        {
            if (node.Parents.Count != 0)
            {
                return FindRoot(node.Parents[0]);
            }

            return node;
        }

        public List<Guid> Bfs(Node node)
        {
            var list = new List<Guid>();
            var queue = new Queue<Node>();

            node.Visited = true;
            queue.Enqueue(node);
            list.Add(node.Id);

            while (queue.Count != 0)
            {
                node = queue.Dequeue();
                foreach (var child in node.Children)
                {
                    if (!child.Visited)
                    {
                        child.Visited = true;
                        queue.Enqueue(child);
                        list.Add(child.Id);
                    }
                }
            }
            
            return list;
        }

        public class Node
        {
            public Guid Id { get; set; }
            public bool Visited { get; set; }
            private IList<Node>? _children;
            public IList<Node> Children
            {
                get
                {
                    if (_children == null)
                    {
                        _children = new List<Node>();
                    }

                    return _children;
                }
            }
            private IList<Node>? _parents;
            public IList<Node> Parents
            {
                get
                {
                    if (_parents == null)
                    {
                        _parents = new List<Node>();
                    }

                    return _parents;
                }
            }
        }
    }
}
﻿using MediatR;

namespace TODO_User.Application.Feature.Queries.GetJobs
{
    public class GetJobQuery : IRequest<List<GetJobsDto>>
    {
        public string UserEmail { get; set; }

        public GetJobQuery(string userEmail)
        {
            UserEmail = userEmail;
        }
    }
}

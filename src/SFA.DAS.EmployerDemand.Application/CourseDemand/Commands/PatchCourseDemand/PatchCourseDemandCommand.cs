using System;
using MediatR;
using Microsoft.AspNetCore.JsonPatch;

namespace SFA.DAS.EmployerDemand.Application.CourseDemand.Commands.PatchCourseDemand
{
    public class PatchCourseDemandCommand : IRequest<PatchCourseDemandCommandResponse>
    {
        public Guid Id { get; set; }
        public JsonPatchDocument<Domain.Models.PatchCourseDemand> Patch { get ; set ; }
    }
}
﻿using System;

namespace SFA.DAS.EmployerDemand.Domain.Entities
{
    public class ProviderInterest
    {
        public ProviderInterest()
        {
            
        }

        public ProviderInterest(Guid id, Models.ProviderInterests source, Guid employerDemandId)
        {
            Id = id;
            EmployerDemandId = employerDemandId;
            Ukprn = source.Ukprn;
            Email = source.Email;
            Phone = source.Phone;
            Website = source.Website;
        }

        public Guid Id { get ; set ; }

        public Guid EmployerDemandId { get; set; }
        public int Ukprn { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Website { get; set; }
        public DateTime DateCreated { get; set; }
        public virtual CourseDemand CourseDemand { get ; set ; }
    }
}
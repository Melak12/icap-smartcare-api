using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SmartcareAPI.Entities;

namespace SmartcareAPI.Repo
{
    public interface IArtRepo
    {
        public Task<ArtPatient> GetArtPatients(long mrn);
    }
}
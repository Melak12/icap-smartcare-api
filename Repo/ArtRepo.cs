using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using SmartcareAPI.Database;
using SmartcareAPI.Entities;

namespace SmartcareAPI.Repo
{
    public class ArtRepo:IArtRepo
    {
        private readonly DapperContext _context;
        public ArtRepo(DapperContext context)
    {
        _context = context;
    }

        public async Task<ArtPatient> GetArtPatients(long mrn)
        {
            var query = @"select registration.patientid as mrn,
registration.FirstName + ' ' + registration.MiddleName + ' ' + registration.SurName AS FullName,
dbo.fn_Age(registration.DateOfBirth, GETDATE()) AS Age,
registration.Sex,
CASE
			WHEN crt.art_dose = '0'
			 THEN '30'
			WHEN crt.art_dose = '1'
			 THEN '60'
			WHEN crt.art_dose = '2'
			 THEN '90'
			WHEN crt.art_dose = '3'
			 THEN '120'
			WHEN crt.art_dose = '4'
			 THEN '150'
			WHEN crt.art_dose = '5'
			 THEN '180'
			 ELSE ''
		 END as ARVDoseDays,
		 dbo.fn_GetARTRegimenCode(crt.ARVDispendsedDose) as Drug ,
		 [dbo].[fn_GregorianToEthiopianDate](crt.FollowupDate) as Followupdate,
		 [dbo].[fn_GregorianToEthiopianDate](crt.next_visit_date) as nextvisitdate
 from 
dbo.crtEthiopiaARTVisit as crt
INNER JOIN 
			(SELECT PatientId, MAX(FollowUpDate) AS FollowupDate  
			FROM dbo.crtEthiopiaARTVisit  
			WHERE(Deprecated = 0)   
			and FollowUpDate <= getdate()  
			GROUP BY PatientId) as latest  
on latest.PatientId = crt.PatientId
and latest.FollowUpDate = crt.FollowUpDate

inner join registration 
on crt.PatientId = registration.pid
/** you will put mrn here to filter for one patient **/
where registration.patientid = @mrn";
    using (var connection = _context.CreateConnection())
    {
        var patient = await connection.QuerySingleOrDefaultAsync<ArtPatient>(query, new {mrn});
        return patient;
    }
        }
    }
}
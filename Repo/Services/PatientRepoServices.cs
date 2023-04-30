using Doctor_Appointment.Models;

namespace Doctor_Appointment.Repo.Services
{
    public class PatientRepoServices : IPatientRepo
    {
        public MedcareDbContext Context { get; }

        public PatientRepoServices(MedcareDbContext context)
        {
            Context = context;
        }
        public List<Patient> GetAll()
        {
           return Context.Patients.ToList();
        }

        public Patient GetById(int id)
        {
            return Context.Patients.FirstOrDefault(p => p.PatientID == id);
        }
        public void Insert(Patient patient)
        {
            Context.Add(patient);
            Context.SaveChanges();
        }

        public void Update(int id, Patient patient)
        {
            var pat = Context.Patients.FirstOrDefault(p => p.PatientID == id);
            
            pat.Email=patient.Email;
            pat.Address=patient.Address;
            pat.Age=patient.Age;
            pat.FullName=patient.FullName;
            pat.PhonNum=patient.PhonNum;
            Context.Update(pat);
            Context.SaveChanges();
        }
        public void Delete(int id)
        {
            var del_pat = Context.Patients.FirstOrDefault(p => p.PatientID == id);
            Context.Remove(del_pat);
            Context.SaveChanges();
        }
    }
}

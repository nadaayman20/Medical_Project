using Doctor_Appointment.Models;

namespace Doctor_Appointment.Repo
{
    public interface IPatientRepo
    {
        public List<Patient> GetAll();
        public Patient GetById(int id);
        public void Insert(Patient patient);
        public void Update(int id, Patient patient);
        public void Delete(int id);
    }
}

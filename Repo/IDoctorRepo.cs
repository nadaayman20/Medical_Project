using Doctor_Appointment.Models;

namespace Doctor_Appointment.Repo
{
    public interface IDoctorRepo
    {
        public List<Doctor> GetAll();
        public Doctor GetById(int id);
        public void Insert(Doctor doctor);
        public void Update(int id, Doctor doctor);
        public void Delete(int id);
    }
}

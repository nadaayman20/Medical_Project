using Doctor_Appointment.Models;

namespace Doctor_Appointment.Repo.Services
{
    public class AppointementRepoServices : IAppointmentRepo
    {
        public MedcareDbContext Context { get; }

        public AppointementRepoServices(MedcareDbContext context)
        {
            Context = context;
        }

        public List<Appointment> GetAll()
        {
           return Context.Appointments.ToList();
        }

        public Appointment GetById(int DocId , int PatId)
        {
            return Context.Appointments.Where(d => d.DoctorID == DocId && d.PatientID == PatId).FirstOrDefault();

            
        }

        public void Insert(Appointment appointment)
        {
            Context.Add(appointment);
            Context.SaveChanges();
        }

        public void Update(int DocId, int PatId, Appointment appointment)
        {
            var upd_app = Context.Appointments.Where(d => d.DoctorID == DocId && d.PatientID == PatId).FirstOrDefault();
       
            upd_app.AppointmentDay = appointment.AppointmentDay;
            upd_app.AppointmentTime = appointment.AppointmentTime;
            upd_app.MedicalHistory = appointment.MedicalHistory;

            Context.Update(upd_app);
            Context.SaveChanges();

        }
        public void Delete(int DocId, int PatId)
        {
            var del_app = Context.Appointments.Where(d => d.DoctorID == DocId && d.PatientID == PatId).FirstOrDefault();
            Context.Appointments.Remove(del_app);
            Context.SaveChanges();
        }
    }
}

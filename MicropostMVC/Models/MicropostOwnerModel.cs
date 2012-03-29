namespace MicropostMVC.Models
{
    public class MicropostOwnerModel
    {
        public UserModel Owner { get; set; }
        public MicropostModel Micropost { get; set; }
    }
}
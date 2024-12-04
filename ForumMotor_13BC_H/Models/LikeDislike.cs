namespace ForumMotor_13BC_H.Models
{
    public class LikeDislike
    {
        public int Id { get; set; }
        public virtual User User { get; set; }
        public int PostId { get; set; }
        public virtual Post Post { get; set; }
        public int Like { get; set; }

    }
}

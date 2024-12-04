namespace ForumMotor_13BC_H.Models
{
    public class Post
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public string UserId { get; set; }
        public int TopicId{ get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }

        public int Reply { get; set; }
        public bool IsDeleted { get; set; }
        public string? DeleteReason { get; set; }

        public virtual User User { get; set; }
        public virtual Topic Topic { get; set; }
        public virtual ICollection<LikeDislike> LikeDislikes { get; set; }
    }
}

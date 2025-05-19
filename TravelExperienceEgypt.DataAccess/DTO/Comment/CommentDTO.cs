namespace TravelExperienceEgypt.DataAccess.DTO;
public class CommentDTO
{
    public int UserId { get; set; }
    public int PostId { get; set; }
    public string Description { get; set; } = null!;
}
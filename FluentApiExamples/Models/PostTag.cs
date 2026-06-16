namespace FluentApiExamples.Models;

public class PostTag {
    /*
        Conventional key names. 
        
        Comment and uncomment appropriate blocks
        in the ApplicationDbContext's OnModelConfiguring method to have
        EF Core setup database as intended.
    */
    public int PostId { get; set; }
    public int TagId { get; set; }

    /*
        Non-conventional key names.

        Comment and uncomment appropriate blocks
        in the ApplicationDbContext's OnModelConfiguring method to have
        EF Core setup database as intended.
    */
    // public int Pid { get; set; }
    // public int Tid { get; set; }
}
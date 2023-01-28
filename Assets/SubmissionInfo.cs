
// This class contains metadata for your submission. It plugs into some of our
// grading tools to extract your game/team details. Ensure all Gradescope tests
// pass when submitting, as these do some basic checks of this file.
public static class SubmissionInfo
{
    // TASK: Fill out all team + team member details below by replacing the
    // content of the strings. Also ensure you read the specification carefully
    // for extra details related to use of this file.

    // URL to your group's project 2 repository on GitHub.
    public static readonly string RepoURL = "https://github.com/COMP30019/project-2-the-team";
    
    // Come up with a team name below (plain text, no more than 50 chars).
    public static readonly string TeamName = "The Team";
    
    // List every team member below. Ensure student names/emails match official
    // UniMelb records exactly (e.g. avoid nicknames or aliases).
    public static readonly TeamMember[] Team = new[]
    {
        new TeamMember("Yu Cao", "yccao4@student.unimelb.edu.au"),
        new TeamMember("Xi Chen", "xcch7@student.unimelb.edu.au"),
        new TeamMember("Andy Jun Cheng Low", "andyjuncheng@student.unimelb.edu.au"),
        // Remove the following line if you have a group of 3
        new TeamMember("Bowen Fan", "bffa@student.unimelb.edu.au"), 
    };

    // This may be a "working title" to begin with, but ensure it is final by
    // the video milestone deadline (plain text, no more than 50 chars).
    public static readonly string GameName = "The Tower";

    // Write a brief blurb of your game, no more than 200 words. Again, ensure
    // this is final by the video milestone deadline.
    public static readonly string GameBlurb = 
@"The Tower is a tower-defense game that begins with the player selecting
the position of the tower. Enemieswill spawn around the tower and try to
walk towards the center to attackthe player. The players' goal is to survive
as long as you can under the attacks of the enemies.
        
At the start of each game, a random map will be procedurally generated
including many terrain properties such as obstacles including lakes, trees,
rocks etc. The player attacks by clicking with the mouse to fire attacking
projectiles to kill the enemies. Props including money will also drop from
the sky and can be collected using mouse clicks. The enemies will be generated
in waves with a short time in between.
        
There will be two shop systems in the game. The battle shop is always
accessible throughout the game, and the upgrade shop is only accessible at
the end of each wave. In the battle shop, the player is able to buy quick buffs
that show effect immediately during an enemy attack wave. In the upgrade shop,
the player can purchase new attacking ability, stat upgrades and repairs.
";
    
    // By the gameplay video milestone deadline this should be a direct link
    // to a YouTube video upload containing your video. Ensure "Made for kids"
    // is turned off in the video settings. 
    public static readonly string GameplayVideo = "https://youtu.be/bPIBJF3TSLo";
    
    // No more info to fill out!
    // Please don't modify anything below here.
    public readonly struct TeamMember
    {
        public TeamMember(string name, string email)
        {
            Name = name;
            Email = email;
        }

        public string Name { get; }
        public string Email { get; }
    }
}

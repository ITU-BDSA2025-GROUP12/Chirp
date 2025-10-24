namespace Chirp.Razor.Pages;

public class CheepDTO
{ //not sure about the visibility modifiers in here
    public required string Author {get; set;}
    public required string Text {get; set;}
    public required string Time {get; set;}
}
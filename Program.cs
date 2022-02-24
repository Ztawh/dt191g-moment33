using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<MusicDb>(options => options.UseSqlite("Data Source=MusicDb.db"));
var app = builder.Build();

// get
app.MapGet("/", () => "Välkommen");
app.MapGet("/music", async(MusicDb db) =>
    await db.Music.ToListAsync());
    
// get ID
app.MapGet("/music/{id}", async(int id, MusicDb db) =>
    await db.Music.FindAsync(id)
    is Music music ? Results.Ok(music) : Results.NotFound()
    );
   
// post
app.MapPost("/music", async (Music music, MusicDb db) =>
{
    db.Music.Add(music);
    await db.SaveChangesAsync();
    return Results.Created("Music added: ", music);
});

// delete
app.MapDelete("/music/{id}", async (int id, MusicDb db) =>
{
    var song = await db.Music.FindAsync(id);
    if (song == null)
    {
        return Results.NoContent();
    }

    db.Music.Remove(song);
    await db.SaveChangesAsync();
    return Results.Ok("Song removed");

});

// put
app.MapPut("/music/{id}", async (int id, Music newSongData, MusicDb db) =>
{
    var song = await db.Music.FindAsync(id);
    if (song == null)
    {
        return Results.NoContent();
    }

    song.Artist = newSongData.Artist;
    song.SongTitle = newSongData.SongTitle;
    song.SongLength = newSongData.SongLength;
    song.Genre = newSongData.Genre;
    await db.SaveChangesAsync();
    return Results.Ok("Song updated");
});

app.Run();

class Music
{
    public int Id { get; set; }
    public string Artist { get; set; }
    public string SongTitle { get; set; }
    public int SongLength { get; set; }
    public string Genre { get; set; }
}

class MusicDb : DbContext
{
    public MusicDb(DbContextOptions<MusicDb> options) : base(options){}
    public DbSet<Music> Music => Set<Music>();
}
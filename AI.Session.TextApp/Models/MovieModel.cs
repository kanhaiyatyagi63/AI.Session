// -------------------------------------------------------------------
// <copyright file="MovieModel.cs" company="Kanhaya Tyagi">
// Copyright 2025 kanhaiyatyagi63 All rights reserved.
// </copyright>
// -------------------------------------------------------------------

using Microsoft.Extensions.VectorData;

namespace AI.Session.TextApp.Models;

/// <summary>
/// Represents a movie with its identifier, title, and description.
/// </summary>
internal class MovieModel
{
    /// <summary>
    /// Gets or sets the unique identifier for the movie.
    /// </summary>
    [VectorStoreKey(StorageName = "_id")]
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the title of the movie.
    /// </summary>
    [VectorStoreData]
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the description of the movie.
    /// </summary>
    [VectorStoreData]
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the vector represented as a read-only memory block of single-precision floating-point values.
    /// </summary>
    [VectorStoreVector(Dimensions: 384,
        DistanceFunction = DistanceFunction.CosineSimilarity)]
    public ReadOnlyMemory<float> Vector { get; set; }

    /// <summary>
    /// Retrieves a predefined list of default movies.
    /// </summary>
    /// <remarks>The returned list contains a collection of popular movies, each represented by a <see
    /// cref="MovieModel"/>  object with predefined values for properties such as <c>Id</c>, <c>Title</c>, and
    /// <c>Description</c>. This method is useful for scenarios where a default set of movies is required, such as
    /// seeding data  or providing sample content.</remarks>
    /// <returns>A <see cref="List{T}"/> of <see cref="MovieModel"/> objects representing the default movies.</returns>
    public static List<MovieModel> Seed()
    {
        return
        [
            new ()
            {
                Id = 1,
                Title = "Inception",
                Description = "A thief who steals corporate secrets through the use of dream-sharing technology is given the inverse task of planting an idea into the mind of a C.E.O.",
            },
            new()
            {
                Id = 2,
                Title = "The Matrix",
                Description = "A computer hacker learns from mysterious rebels about the true nature of his reality and his role in the war against its controllers.",
            },
            new()
            {
                Id = 3,
                Title = "Interstellar",
                Description = "A team of explorers travel through a wormhole in space in an attempt to ensure humanity's survival.",
            },
            new()
            {
                Id = 4,
                Title = "The Dark Knight",
                Description = "When the menace known as the Joker emerges from his mysterious past, he wreaks havoc and chaos on the people of Gotham. The Dark Knight must accept one of the greatest psychological and physical tests of his ability to fight injustice.",
            },
            new()
            {
                Id = 5,
                Title = "Pulp Fiction",
                Description = "The lives of two mob hitmen, a boxer, a gangster's wife, and a pair of diner bandits intertwine in four tales of violence and redemption.",
            },
            new()
            {
                Id = 6,
                Title = "Forrest Gump",
                Description = "The presidencies of Kennedy and Johnson, the Vietnam War, the Watergate scandal and other historical events unfold from the perspective of an Alabama",
            },
            new()
            {
                Id = 7,
                Title = "The Shawshank Redemption",
                Description = "Two imprisoned men bond over a number of years, finding solace and eventual redemption through acts of common decency.",
            },
            new ()
            {
                Id = 8,
                Title = "The Godfather",
                Description = "The aging patriarch of an organized crime dynasty transfers control of his clandestine empire to his reluctant son.",
            },
            new()
            {
                Id = 9,
                Title = "Fight Club",
                Description = "An insomniac office worker and a devil-may-care soap maker form an underground fight club that evolves into something much, much more.",
            },
            new()
            {
                Id = 10,
                Title = "The Lord of the Rings: The Fellowship of the Ring",
                Description = "A meek Hobbit from the Shire and eight companions set out on a journey to destroy the powerful One Ring and save Middle-earth from the Dark Lord Sauron.",
            },
        ];
    }
}

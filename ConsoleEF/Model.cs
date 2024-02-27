using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System;

// EF Core uses DbContext to query and save data - db structure is defined by entity classes
public class BloggingContext : DbContext
{
    public DbSet<Blog> Blogs { get; set; }   
    public DbSet<Post> Posts { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public string DbPath { get; }

    // constructor
    public BloggingContext()
    {
        DbPath = "blogging.db";
        Database.EnsureCreated();
    }

    // override OnConfiguring to specify the database provider
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite($"Data Source={DbPath}");
    }
}

// blog table
public class Blog {
    // EF Core wants an Id property as primary key
    public int BlogId { get; set; }
    public string Url { get; set; }

    // domain model: blog = list of posts
    public List<Post> Posts { get; }  = new ();
    
}

// post table
public class Post {
    public int PostId { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public int BlogId { get; set; }
    public Blog Blog { get; set; }
    public List<Comment> Comments { get; } = new ();
}

// comments table
public class Comment {
    public int CommentId { get; set; }
    public string Content { get; set; }
    // backlink to post
    public int PostId { get; set; }
    public Post Post { get; set; }
}
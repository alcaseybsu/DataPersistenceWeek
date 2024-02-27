using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

using var db = new BloggingContext();

// eager loading
var blog = db.Blogs.Include(b=>b.Posts).FirstOrDefault();
Console.WriteLine(blog.Posts.Count);

blog.Posts.Add(new Post{Title = "Hello", Content = "Whatever Content"});
db.SaveChanges();
# Example

```cs
 class User
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public DateTime CreatedAt { get; set; }
        }
        
    var user = ObjectBuilder.Builder<User>.New()
                             .SetProperty(u => u.Id, 123)
                             .SetProperty(u => u.Name, "Mohamed")
                             .SetProperty(u => u.CreatedAt, DateTime.Now)
                             .Finish();
                             
                             
```

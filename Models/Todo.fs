namespace FsharpWebapi.Models

open Microsoft.EntityFrameworkCore
open Microsoft.EntityFrameworkCore.Infrastructure

[<CLIMutable>]
type Todo =
    {
        Id: string
        Name: string
        Description: string
        isComplete: bool
    }

type TodoContext(options: DbContextOptions<TodoContext>) =
    inherit DbContext(options)

    [<DefaultValue>]
    val mutable Todo : DbSet<Todo>
    member this.Todos
        with get() = this.Todo
        and set v = this.Todo <- v

namespace FsharpWebapi.Controllers

open System
open System.Collections.Generic
open System.Linq
open System.Threading.Tasks
open Microsoft.AspNetCore.Mvc

open FsharpWebapi.Models
open System.Collections
open Microsoft.AspNetCore.Mvc

[<Route("api/[controller]")>]
[<ApiController>]
type TodoController(context) =
    inherit Controller()
    let _context: TodoContext = context
    // new() = new TodoController(new TodoRepository())
    // new(repository : IRepository<Todo>) as this = {} then
    //     this._repository <- repository

    // [<DefaultValue>]
    // val mutable private _repository : IRepository<Todo>

    [<HttpGet>]
    member this.Get() =
        // let todos = this._repository.GetAll()
        // ActionResult<IList<Todo>>(todos)
        let todos =
            if isNull _context.Todo then
                new List<Todo>()
            else
                _context.Todo.ToList()
        ActionResult<List<Todo>>(todos)

    [<HttpGet("{id}")>]
    member this.Get(id: int) =
        let todo = _context.Todo.SingleOrDefault(fun m -> m.Id = id)
        ActionResult<Todo>(todo)

    [<HttpPost>]
    member this.Post([<FromBody>] todo: Todo) =
        _context.Add(todo) |> ignore
        _context.SaveChanges() |> ignore
        todo.Id

    [<HttpPut("{id}")>]
    member this.Put(id: int, [<FromBody>] todo: Todo) =
        if id <> todo.Id then
            -1
        else
            _context.Update(todo) |> ignore
            _context.SaveChanges() |> ignore
            todo.Id

    [<HttpDelete("{id}")>]
    member this.Delete(id: int) =
        let todo = _context.Todo.SingleOrDefault(fun m -> m.Id = id)
        _context.Remove(todo) |> ignore
        _context.SaveChanges() |> ignore
        todo.Id

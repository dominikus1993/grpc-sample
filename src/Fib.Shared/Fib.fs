namespace Fib.Shared

module Calculator =
    let Calculate n =
        if n = 0 then
            0L
        elif n = 1 then
            1L
        else
            Seq.unfold(fun(x,y)-> Some(x,(y,x + y)))(0L,1L) |> Seq.skip(n - 1) |> Seq.take(n) |> Seq.head

namespace PeanoGenerator

open System.Drawing

module Stuff =
    // defines the directions to move to draw the picture
    type Directions = Left | Right | Up | Down

    // the pattern that start the fractal
    let basePattern = [Down; Down; Right; Up; Up; Right; Down; Down]

    // trasformation that applies on the previous pattern when moving from a tile to the next
    let flipHorizontaly = List.rev >> (List.map (function Left -> Right | Right -> Left | d -> d))
    let flipVertically = List.rev >> (List.map (function Up -> Down | Down -> Up | d -> d))
    let flip = function | Up | Down -> flipHorizontaly | Left | Right -> flipVertically

    // an iteration run: replicates the base pattern to all previous steps
    // and transform it on each steps, then append the n-1 direction
    let iterate s =
        Seq.zip (Seq.scan (fun prevPattern d -> flip d prevPattern) basePattern s) s
        |> Seq.map (fun (s,d) -> s @ [d]) |> Seq.concat

    // reapplies the iteration the given number of time
    let fractal i =
        let rec fractal_i i pattern =
            match i with 0 -> pattern | i -> fractal_i (i-1) (iterate pattern)
        fractal_i i basePattern

    // converts from directions to points to join starting from top-left corner
    let toPoints edgeLegth =
        Seq.scan
            (fun position direction ->
                match direction with
                | Left -> position + Size(-edgeLegth, 0)
                | Right -> position + Size(edgeLegth, 0)
                | Up -> position + Size(0, -edgeLegth)
                | Down -> position + Size(0, edgeLegth)
            ) (Point(0,0))

    // Ze public API
    let GetPeanoPatten (iteration: int, edgeLength: int) =
        fractal iteration |> toPoints edgeLength

//https://leetcode.com/problems/two-sum/
(*
Given an array of integers, return indices of the two numbers such that they add up to a specific target.

You may assume that each input would have exactly one solution.

Example:
Given nums = [2, 7, 11, 15], target = 9,

Because nums[0] + nums[1] = 2 + 7 = 9,
return [0, 1].
*)

open System
open System.Collections.Generic


let twoSum (nums:int[]) target =
    let length = nums.Length
    [for x in 0..length-2 do
        for y in x+1..length-1 do
            if nums.[x]+nums.[y] = target then
                yield (x,y)].Head
    

[<EntryPoint>]
let main argv=
    let nums = [|3;2;4|]
    let tar = 6
    let (x,y) = twoSum nums tar
    printfn "%d,%d" x y
    Console.ReadKey() |> ignore
    0

//https://leetcode.com/problems/two-sum/
/**
Given an array of integers, return indices of the two numbers such that they add up to a specific target.

You may assume that each input would have exactly one solution.

Example:
Given nums = [2, 7, 11, 15], target = 9,

Because nums[0] + nums[1] = 2 + 7 = 9,
return [0, 1].
**/

open System
open System.Collections.Generic


let twoSum (nums:int[]) target =
    let d = dict (Array.mapi( fun i x->(x,i)) nums |>Array.toSeq)
    let l = [for v in d do 
                if d.ContainsKey(target-v.Key) then 
                    yield (v.Value, d.Item(target-v.Key))]
    l.[0]

[<EntryPoint>]
let main argv=
    let nums = [|2;7;11;15|]
    let tar = 9
    let (x,y) = twoSum nums tar
    printfn "%d,%d" x y
    Console.ReadKey() |> ignore
    0
    

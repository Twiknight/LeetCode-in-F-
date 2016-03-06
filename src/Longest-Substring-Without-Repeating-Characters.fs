(*
https://leetcode.com/problems/longest-substring-without-repeating-characters/

Given a string, find the length of the longest substring without repeating characters. 

For example, the longest substring without repeating letters for "abcabcbb" is "abc", which the length is 3. 

For "bbbbb" the longest substring is "b", with the length of 1.
*)

open System

let lengthOfLongestSubstring (s:string) =
    if s.Length<2 then s.Length else
    let arr = s.ToCharArray()
    let rec search (map:Map<Char,int>) (start:int) (ed:int) (m:int)= 
         if ed = arr.Length then Math.Max(ed-start,m)
         else
            let c = arr.[ed]
            if map.ContainsKey(c) then 
                let s1 = Math.Max(map.Item(c),start)
                search (map.Remove(c).Add(c,s1)) (s1+1) (ed+1) (Math.Max(m,ed-s1))
            else search (map.Add(c,ed)) start (ed+1) m
    search Map.empty 0 1 1

[<EntryPoint>]
let main argv=
    let x = lengthOfLongestSubstring "abcabcbb"
    printfn "%d" x
    Console.ReadKey() |> ignore
    0

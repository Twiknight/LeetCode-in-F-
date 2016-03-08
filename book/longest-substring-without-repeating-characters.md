Problem Link: https://leetcode.com/problems/longest-substring-without-repeating-characters/

Solution Source: https://github.com/Twiknight/LeetCode-in-Fsharp/blob/master/code/longest-substring-without-repeating-characters.fs

***

Problem description:
>Given a string, find the length of the longest substring without repeating characters.
>For example, the longest substring without repeating letters for "abcabcbb" is "abc", which the length is 3.
>For "bbbbb" the longest substring is "b", with the length of 1.

First of All, as usual, we do some translation:

1. We have a string which we name it `s`
2. We product the string `s` to a set of all its sub-strings.
3. We need to find a string that fulfills the following constrains:
  1. It contains no repeating characters
  2. It has the max length of strings that fulfill constrain 1
4. Now we return the length of the string we got in rule 3

As it's easy to solve this problem with a brute-force search algorithm following the steps above.

I prefer not to provide the code for such case...
The complexity would be O(n^2) or O(n^3) according to the way you implement the non-repeating filter.

However, you know that there must be better practice.

Let's see how optimize it to O(n) from the brute-force search.

__If you need to optimize an O(n^2) or more complex algorithm when handling something array-like,
A usual way is to use a `HashMap` to do space-time trade-off as we know
that a `HashMap` queries at cost of just O(1).__

That's why I said the complexity of brute-force search would vary on the implementation of non-repeating filter.
Because if you implement the filter with a `HashMap` or `HashSet`,
the cost of determining whether a `string` is non-repeating would be decreased to O(n) level rather than O(n^2) of back-tracking search.

However, the limit of optimize the filter is O(n^2),
as you O(n^2) to do the production (step 2).
That's the performance bottle-neck of this algorithm.

__So can we use `HashMap` one more time to reduce the complexity of the production?__

__Unfortunately, that's impossible.__
I mean you can not work out such a collection with (n+1)*n/2 elements within O(n) time.

The secret is we don't really need to generate the whole production set.
What we want, is to find the __longest__ __non-repeating__ sub-string.

That means:

1.  we don't need to pay any attention to repeating sub-strings;
2.  If I have already met a k-character non-repeating sub-string,
    there's no need to check any sub-string shorter than k.

And a widely known algorithm that works fine when you work on some sub-array or sub-string is called __two-pointers__ or __window__:

You have two pointers `left` and `right` on the array and we call the elements between the two pointers a `window`.
By moving or scaling the `window` we can handle part of the string each time (in this problem, that's sub-string).
In this algorithm，
we move `left` and `right` from the start of the array to the end __only once__
without any back tracking.

Why we can ensure a single iteration will work out the problem?

1. If string `a` is repeating, any string that contains `a` is repeating;
2. If we have found a non-repeating string with k-length, any string shorter than k is passed;

So Let's see how it works on the string `abbc`:

1. We assign `left` and `right` to `0`, so the current longest non-repeating sub-string is `a`;
2. We move `right` to `1`, and find `b` is not in string `a`; Now the longest non-repeating string is `ab`
3. We move `right` to `2`, and find that `b` is in `ab`;
   Now we need to move `left` as we know that any sub-string contains `abb` is repeating;
4. We move `left` to `2` (why not 1?), and `right` to `3`;
5. `bc` is non-repeating but the max length is still 2;
5. We end the search as we reaches the end of the string;

Code for the solution:

```fsharp
let lengthOfLongestSubstring (s:string) =
    if s.Length<2 then s.Length else
    let arr = s.ToCharArray()
    let rec search (map:Map<Char,int>) (start:int) (ed:int) (m:int)=
         if ed = arr.Length then Math.Max(ed-start,m)
         else
            let c = arr.[ed]
            if map.ContainsKey(c) && map.Item(c) >= start then
                let s1 = map.Item(c)
                search (map.Remove(c).Add(c,ed)) (s1+1) (ed+1) (Math.Max(m,ed-s1))
            else search (map.Add(c,ed)) start (ed+1) (Math.Max(m,ed-start+1))
    search (Map.empty.Add(arr.[0],0)) 0 1 1
```

Javascript edition beats 97% on leetcode (Non-Hash implemention):
```javascript
var lengthOfLongestSubstring = function(s) {
    'use strict';
    if(s.length<2){
        return s.length;
    }

    let start = 0,end = 1;
    let subLen = 1;
    let map = new Map();
    map.set(s[0],0);
    for(end;end<s.length;end++){
      let c = s[end];
      if(map.has(c) && map.get(c)>=start){
        start = map.get(c)+1;
      }else{
        let len = end-start+1;
        subLen = subLen>len?subLen:len;
      }
      map.set(c,end);
    }
    return subLen;
};
```

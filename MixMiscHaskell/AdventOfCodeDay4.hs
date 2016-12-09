-- cabal install split
import Data.List.Split
import Data.List

extractRoom :: String -> ([String], Int, [Char]) -- What is the purpose of these function definitions, it compiles fine without them
extractRoom a = 
    (init entire, read (head lastPart) :: Int, init (last lastPart))
    where
        entire = splitOn "-" a
        lastPart = splitOn "[" (last entire)

countLetters :: String -> [(Int, Char)]
countLetters a = reverse $ sort $ nub $ map countSpecificChar a
    where countSpecificChar x = (length (filter (== x) a), x)

toValidAtIndexList :: [(Int, Char)] -> [[Char]]
toValidAtIndexList listOfTuple = map getValidAtThisPos listOfTuple
    where getValidAtThisPos tuple = map (\f -> snd f) (filter ((\z (x,y) -> x == z) (fst tuple)) listOfTuple)

validateRoom :: ([String], Int, [Char]) -> Int
validateRoom (a,b,c) = if isValidRoom c $ toValidAtIndexList (countLetters $ concat a) then b else 0

isValidRoom :: [Char] -> [[Char]] -> Bool
isValidRoom expected actual = all isValid (zip expected actual)
    where isValid (c, possible) = any (== c) possible

main = do  
    putStrLn "Start..."  
    contents <- readFile "input/day4.txt" 
    let linesOfFile = lines contents
    let rooms = map extractRoom linesOfFile
    let result = map validateRoom rooms
    putStrLn $ "Real rooms: " ++ show ( sum result)

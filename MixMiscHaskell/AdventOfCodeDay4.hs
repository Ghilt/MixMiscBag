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

filterValidRooms :: [([String], Int, [Char])] -> [([String], Int, [Char])]
filterValidRooms a = filter isValidRoom a

sumRoomSectorID :: [([String], Int, [Char])] -> Int
sumRoomSectorID list = sum (map sectorId list)
    where sectorId (_,b,_) = b

isValidRoom :: ([String], Int, [Char]) -> Bool
isValidRoom (a,b,c) = lettersMatch c $ toValidAtIndexList (countLetters $ concat a)

lettersMatch :: [Char] -> [[Char]] -> Bool
lettersMatch expected actual = all isValid (zip expected actual)
    where isValid (c, possible) = any (== c) possible

decipherRooms :: ([String], Int, [Char]) -> ([String], Int, [Char])
decipherRooms (a,b,c) = (map shiftIt a,b,c)
    where shiftIt x = map (shiftCipher b) x

shiftCipher ::  Int -> Char -> Char
shiftCipher x c = (iterate alphaMod c) !! x

alphaMod :: Char -> Char
alphaMod c 
        | c == 'z' = 'a'
        | otherwise = succ c

containsNorth :: ([String], Int, [Char]) -> Bool
containsNorth (a,b,c) = isInfixOf "north" (concat a)

main = do  
    putStrLn "Start..."  
    contents <- readFile "input/day4.txt" 
    let linesOfFile = lines contents
    let rooms = map extractRoom linesOfFile
    let validRooms = filterValidRooms rooms
    let result = sumRoomSectorID validRooms
    let decipheredRooms = map decipherRooms rooms
    let northPole = find containsNorth decipheredRooms
    putStrLn $ "Real rooms: " ++ show (result)
    putStrLn $ "Northpole Objects: " ++ show(northPole)

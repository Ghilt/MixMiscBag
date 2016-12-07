
getSides :: String -> [Int]
getSides a = map read (words a)
isValidTriangle :: [Int] -> Bool
isValidTriangle [a, b, c] = ((a + b) > c) && ((a + c) > b) && ((b + c) > a) 
countTriangles :: [[Int]] -> Int
countTriangles a = length (filter isValidTriangle a)

main = do  
    putStrLn "Start..."  
    contents <- readFile "input/day3.txt" 
    let linesOfFile = lines contents
    let listOfSides = map getSides linesOfFile
    let amountOfTriangles = countTriangles listOfSides 
    putStrLn $ "Amount of valid triangles: " ++ show(amountOfTriangles)
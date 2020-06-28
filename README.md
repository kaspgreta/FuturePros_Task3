# FuturePros_Task3
It is the third task of the FuturePros in Danske Bank programme.

The main goal of this task is to determine, if the array's last element is reachable.

Application are written in C#, using Visual Studio IDE.

In this application were defined three main tasks - accept array of digits, determine is the goal is reachable and provide most efficient way.

To find the most efficient path, there was used the Dijkstra algorithm, which helped to find the shortest path between the graphs. For graphs there was created a matrix, which was defined by 0 and 1 values. After initializing the matrix, the shortest path between the points of the matrix was searched.

Application also stores all arrays and results into file. Later on user can ask to show all record as a list or individually using swtch statement. Application can accept a batch of arrays and process them all, too. It can not process all arrays and use already calculated results.

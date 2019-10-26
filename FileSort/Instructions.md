To start the test you will <a href="https://drive.google.com/file/d/14G9OnAj5J6WPYZt7C14X1-zVAgOHAtxi/view?usp=sharing">download a large file</a>. You will then use the instructions below to write
a console application that sorts the file in a memory-efficient manner. The answer to the test is hidden
in the sorted file. You should be comfortable with reading and writing text files, working with arrays,
and creating basic console applications.
Before you begin, download the <a href="https://drive.google.com/file/d/14G9OnAj5J6WPYZt7C14X1-zVAgOHAtxi/view?usp=sharing">bigfile.txt.gz</a> file and save it on your system. It is about 397
Megabytes. The application will require that you have about 1.6 Gb of free disk space. If this is a
problem let me know.
Tips..
You can decide what kinds of objects and structures to use to accomplish these tasks. For
example, if the instructions call for you to store strings in an array, you can use a List, a
string[], and ArrayList, or whatever you think gets the job done adequately.
Even though there are other ways to accomplish the task, possibly even simpler or faster
ways, we're looking for an application that more or less follows these instructions.

Test Instructions
1. Uncompress the gzip file in .Net
2. Read the first 5000 lines from bigfile.txt and store them in an array.
Lines are terminated with standard Windows CRLF characters, so that a
StreamReader.ReadLine() will work fine to read each line.
3. Sort the array alphanumerically.
Hint: Use any type or array or collection you think is appropriate.
4. Write the sorted array of lines to sequentially-named temporary file, like "file_0.txt"
5. Repeat steps 2 through 4, reading 5000 lines at a time from the file and creating temporary files,
like "file_1.txt", "file_2.txt", etc...
6. When the entire file is read and stored in temporary files, open all of the temporary files and
read the first line from each file.
Create a new sorted array to store the first line from each file. Call this "mergedLinesArray".
Store the opened file (probably a StreamReader object) in another array. Call this "fileArray".
Create a Dictionary or Hashtable to keep track of which line came from which file. Call this
"lineFileLookup".
7. Sort the mergedLinesArray alphanumerically.
8. Take the smallest (alphanumerically) line from the mergedLinesArray. This should be found at
index 0. Call this "smallestLine".
9. Take the 29th character, e.g. smallestLine.Substring(28,1), from "smallestLine" and append it to
your answer (at this point, just a blank string).
10. Remove the smallestLine from mergedLinesArray.
11. Read the next line in from the same file that contained the mergedLinesArray, using your
lineFileLookup structure to figure out which file it was in.
12. Add in this new line at the appropriate spot in your mergedLinesArray.
Adding it directly to the mergedLinesArray in the correct (sorted) location can be more
efficient than adding it at the end and resorting your mergedLinesArray each time.
Be sure to keep track of what file each line came from in your lineFileLookup. Remove the
lines you have already processed and add new ones as you read them from the files.
13. Continue this process until all the lines have been read from all the files. When you are
complete you will have sorted the file in a memory-efficient manner.
14. By taking the 29th character of every line, properly sorted, you should have a long string which
contains the answer.
15. Write the answer string to a compressed text file (gzip or zip)


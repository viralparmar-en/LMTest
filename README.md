# 📦 LMTest File Parser

This project is a console application designed to parse bank-specific CSV files and generate structured output files.

---

## 🚀 Getting Started

Follow these steps to clone, build, and run the application:

### 1. Clone the Git Repository

```bash
git clone https://github.com/viralparmar-en/LMTest.git
```

###  2. Navigate to the Solution Folder
```bash 
cd LMTestShow 
```
### 3. Build the Solution
```bash
dotnet buildShow 
```

Once the build is successful, proceed to the console application folder:
```bash
cd LMTestFileParser.ConsoleShow 
```
### 4. Publish the Console Application
```bash
dotnet publishShow 
```

### 5. Run the Application
Navigate to the published output folder:
```bash
cd bin/Release/netX.X/publishShow 
```

Replace netX.X with your target framework version (e.g., net6.0, net7.0).

### 6. Run the application with the following command:

```bash
LMTestFileParser.Console.exe <BankName> <Filepath.csv>Show 
```
📁 Output
The parsed output file will be created in:
OutputFiles/<BankName>


✅ Prerequisites

.NET SDK installed
Git installed
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
cd LMTest 
```
### 3. Build the Solution
```bash
dotnet build 
```

### 4. Once the build is successful, proceed to the console application folder:
```bash
cd LMTestFileParser.Console 
```
### 5. Publish the Console Application
```bash
dotnet publish 
```

### 6. Run the Application
Navigate to the published output folder:
```bash
cd bin/Release/netX.X/publish 
```

Replace netX.X with your target framework version (e.g., net6.0, net7.0).

### 7. Run the application with the following command:

```bash
LMTestFileParser.Console.exe <BankName> <Filepath.csv> 
```
📁 Output
The parsed output file will be created in:
OutputFiles/<BankName>


✅ Prerequisites

.NET SDK installed
Git installed
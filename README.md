# AndroidIntelliTool

AndroidIntelliTool is a desktop tool for Windows designed to simplify various development and management tasks for Android devices.

## Features

*   Wireless ADB connection management.
*   Device file explorer.
*   Real-time Logcat viewer.
*   Integration with Scrcpy for device screen mirroring.

## How to Compile

To build the project from source, you will need the **.NET Core SDK 3.1**.

1.  **Clone the repository:**
    ```sh
    git clone <repository-URL>
    cd AndroidIntelliTool
    ```

2.  **Restore project dependencies:**
    ```sh
    dotnet restore AndroidIntelliTool.sln
    ```

3.  **Build the solution in Release mode:**
    ```sh
    dotnet build AndroidIntelliTool.sln --configuration Release
    ```

The executable (`AndroidIntelliTool.exe`) and its dependencies will be located in the `AndroidIntelliTool/bin/Release/` directory.

## How to Use

Once you have compiled the project, simply run the `AndroidIntelliTool.exe` file located in the `AndroidIntelliTool/bin/Release/` folder.

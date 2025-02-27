# NovaPanel - Windows Server Web Operation Panel

**NovaPanel** is a powerful Web operation panel designed for Windows Server, aiming to simplify server management and improve operation efficiency. Through the simulated task manager, resource manager and flexible scheduled tasks, NovaPanel provides you with an intuitive and efficient server management platform.

## Main Features

* **Windows Server Exclusive:** Optimized for Windows Server, providing a stable and efficient operation experience.

* **Simulated Task Manager and Resource Manager:** Intuitive interface, real-time monitoring of server resource usage, easy management of processes and services.

* **Flexible Scheduled Tasks:** Support Python, JavaScript, C#, PowerShell scripts and Bat batch processing to meet various automation needs.

* **Multi-language support:** Provides a multi-language interface for global users.

* **Custom Project Support:** Supports the creation of new C#, website and other projects for easy and rapid deployment and management.
* **Flexible user rights management:** Fine-grained user rights control to ensure server security.
* **Secure login mechanism:** Adopt secure entry + Token mechanism to ensure the security of the login process.
* **Domain name binding restriction:** You can enable access to only bound domain names to enhance server security.
* **Anti-SQL injection:** Built-in anti-SQL injection mechanism to effectively prevent malicious attacks.

## Feature highlights

* **Intuitive monitoring:** Real-time view of CPU, memory, disk and network usage to quickly locate performance bottlenecks.
* **Powerful automation:** Automated operation and maintenance through scheduled tasks to improve work efficiency.
* **Convenient project management:** Easily create, deploy and manage various projects to simplify the development process.
* **Comprehensive security protection:** Multiple security measures to ensure server and data security.
* **Theme customization:**
* Modify the styles of some components by modifying `wwwwroot\style\app.css`
* You can change the default theme settings by modifying the `config.json` file.
* You can use the browser console to temporarily switch the theme by executing the `document.body.classList.toggle("theme-toggle")` command.

## Quick Start

1. **Install .NET SDK:**
* Install .NET SDK 8.0 or 9.0 or higher.
* You can change the .NET SDK version in the `NovaPanel/NovaPanel.csproj` file.
2. **Clone the source code:**
* Clone the NovaPanel source code using Git:
```bash
git clone https://github.com/NovaConnect/NovaPanel.git
```
3. **Enter the project directory:**
* Open the terminal and navigate to the NovaPanel project directory:
```bash
cd NovaPanel/NovaPanel
```
4. **Run the software:**
* Run NovaPanel using .NET CLI:
```bash
dotnet run
```

## Instructions

* **Dashboard:** View server resource usage and system information.
* **Task Manager:** Manage processes and services, end or restart processes.
* **Resource Manager:** Manage files and folders, upload, download, and delete files.
* **Scheduled tasks:** Create and manage scheduled tasks to achieve automated operation and maintenance.
* **Project Management:** Create and manage projects, deploy websites and applications.
* **User Management:** Manage users and permissions, assign roles.
* **Security Settings:** Configure security options, set domain binding and secure entry.
* **Theme Settings:** Change the theme through `config.json` or browser console.

## Contribution

Welcome to contribute code, make suggestions or report issues. Please participate in the project through GitHub Issues or Pull Requests.

## Contact Information

If you have any questions or suggestions, please contact us through the following methods:

* My work email: [Sm4Z0n3Mua@gmail.com]
* GitHub: [NovaConnect Community](https://github.com/NovaConnect)

## Acknowledgements
* **Third-party library support:**
* Use `Blazor.ContextMenu` to provide right-click menu functionality.
* Use `Shadcn/UI` to provide a beautiful and modern user interface.
* Thanks to all developers and users who have contributed to this project.

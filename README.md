# PaperReviewAPI

A minimal backend service implementing a **configurable workflow engine (state machine API)** as part of Infonetica's Software Engineer Intern assignment.

This system enables clients to:

* Define custom workflows (states + actions)
* Start workflow instances based on a definition
* Transition instances via validated actions
* Inspect workflow definitions and instances

---

## 🚀 Tech Stack

* **Language**: C#
* **Framework**: .NET 8 (ASP.NET Core Web API - Minimal API style)
* **Persistence**: In-memory (no database)
* **API Testing**: Swagger (auto-generated), Postman supported

---

## 📁 Folder Structure

```
PaperReviewAPI/
├── Controllers/
│   ├── InstancesController.cs
│   └── WorkflowsController.cs
├── Data/
│   ├── InstanceStore.cs
│   └── WorkflowStore.cs
├── Models/
│   ├── PaperInstance.cs
│   └── Workflow.cs
├── Program.cs
├── PaperReviewAPI.csproj
└── README.md
```

---

## 🛠️ Setup Instructions

### ✅ Prerequisites

* [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
* Visual Studio Code or any C#-compatible editor

### ⚙️ Run the Project

```bash
# Navigate to the project root
cd PaperReviewAPI

# Run the application
dotnet run
```

The server will start at: `http://localhost:5050`

### 📓 API Documentation

Swagger UI available at: `http://localhost:5050/swagger`

---

## 📌 API Endpoints

### 🔧 Workflow Configuration

* `POST /workflow` — Create a new workflow definition
* `GET /workflows` — List all workflow names
* `GET /workflow/{name}` — Get full workflow definition by name

### ▶️ Workflow Runtime

* `POST /instances` — Start a new workflow instance
* `GET /instances` — List all instances
* `GET /instances/{id}` — Fetch details of one instance
* `POST /instances/execute` — Execute an action on an instance

---

## 📬 Sample API Usage (via Postman or Swagger)

### 1. Create a Workflow

**POST** `/workflow`

```json
{
  "name": "paper-review",
  "states": [
    { "name": "submitted", "isInitial": true },
    { "name": "under_review" },
    { "name": "accepted" },
    { "name": "rejected" }
  ],
  "actions": [
    {
      "name": "review",
      "fromStates": ["submitted"],
      "toState": "under_review",
      "enabled": true
    },
    {
      "name": "accept",
      "fromStates": ["under_review"],
      "toState": "accepted",
      "enabled": true
    },
    {
      "name": "reject",
      "fromStates": ["submitted", "under_review"],
      "toState": "rejected",
      "enabled": true
    }
  ]
}
```

### 2. Start an Instance

**POST** `/instances`

```json
{
  "workflowName": "paper-review",
  "title": "Understanding Transformers",
  "author": "Lagnajit Saha"
}
```

### 3. Execute an Action

**POST** `/instances/execute`

```json
{
  "id": 1,
  "actionName": "review"
}
```

---

## 📌 Business Rules & Validations

* Only one initial state (`isInitial: true`) allowed per workflow.
* Action execution is allowed *only if*:

  * The action exists and is enabled.
  * Current state is listed in `fromStates`.
* Rejected or Accepted instances can't perform further actions (treated as final).
* Error messages are descriptive and helpful.

---

## 🔐 Assumptions

* States are uniquely named within a workflow.
* Workflows and Instances are persisted **in-memory** (for simplicity).
* Action names are case-sensitive.

---

## ✅ Sample Edge Cases Tested

* Duplicate workflow creation → 400 BadRequest
* Action not valid from current state → 400 BadRequest
* Disabled action or unknown workflow → 404 / 400
* Accepted or Rejected instances → blocked from further transitions

---

## 📦 Future Improvements

* Persistence using JSON/YAML file storage
* Admin interface to view workflows & states
* WebSocket updates on instance transitions
* Role-based auth for restricted action control

---


## 👨‍💻 Author

[Lagnajit Saha](https://github.com/LagnajitSaha)

---

## 🔗 Submission

This project is submitted as part of the Infonetica Software Engineer Internship Take-Home Assignment. See repo: [https://github.com/LagnajitSaha/PaperReviewAPI](https://github.com/LagnajitSaha/PaperReviewAPI)

import {
  Task,
  TasksResponse,
  CreateTaskRequest,
  UpdateTaskRequest,
  TaskStatus,
} from "@/types/task";

const API_BASE_URL = "http://localhost:5283/api";

export class TaskService {
  static async getAllTasks(
    title?: string,
    status?: TaskStatus,
    pageIndex: number = 1,
    pageSize: number = 10,
  ): Promise<TasksResponse> {
    const params = new URLSearchParams();

    if (title) params.append("title", title);
    if (status) params.append("status", status.toString());
    params.append("pageIndex", pageIndex.toString());
    params.append("pageSize", pageSize.toString());

    const response = await fetch(`${API_BASE_URL}/tasks?${params}`);
    if (!response.ok) throw new Error("Failed to fetch tasks");

    return response.json();
  }

  static async getTaskById(id: number): Promise<Task> {
    const response = await fetch(`${API_BASE_URL}/tasks/${id}`);
    if (!response.ok) throw new Error("Failed to fetch task");

    return response.json();
  }

  static async createTask(task: CreateTaskRequest): Promise<Task> {
    const response = await fetch(`${API_BASE_URL}/tasks`, {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify(task),
    });

    if (!response.ok) throw new Error("Failed to create task");
    return response.json();
  }

  static async updateTask(id: number, task: UpdateTaskRequest): Promise<Task> {
    const response = await fetch(`${API_BASE_URL}/tasks/${id}`, {
      method: "PATCH",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify(task),
    });

    if (!response.ok) throw new Error("Failed to update task");
    return response.json();
  }

  static async deleteTask(id: number): Promise<void> {
    const response = await fetch(`${API_BASE_URL}/tasks/${id}`, {
      method: "DELETE",
    });

    if (!response.ok) throw new Error("Failed to delete task");
  }
}

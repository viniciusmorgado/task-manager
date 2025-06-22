export interface Task {
  id: number;
  title: string;
  description: string;
  status: TaskStatus;
  createdAt: string;
  completedAt: string | null;
  createdById: string;
}

export enum TaskStatus {
  TODO = 1,
  IN_PROGRESS = 2,
  COMPLETED = 3,
}

export interface TasksResponse {
  pageIndex: number;
  pageSize: number;
  count: number;
  data: Task[];
}

export interface CreateTaskRequest {
  title: string;
  description: string;
  createdById: string;
}

export interface UpdateTaskRequest {
  title?: string;
  description?: string;
  status?: TaskStatus;
  createdById: string;
}

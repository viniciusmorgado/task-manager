export type TaskStatus = 1 | 2 | 3;

export interface Task {
  id: number;
  title: string;
  description: string;
  status: TaskStatus;
  createdAt: string;
  completedAt?: string | null;
  createdBy: string;
}

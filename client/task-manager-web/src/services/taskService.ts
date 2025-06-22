import { Task } from "@/types/task";

const API_URL = "http://localhost:5283/api/tasks";

export async function getAllTasks(): Promise<Task[]> {
  const res = await fetch(API_URL, { cache: "no-store" });
  if (!res.ok) throw new Error("Erro ao buscar tarefas");
  const data = await res.json();

  return data.data;
}

export async function getTaskById(id: number): Promise<Task> {
  const res = await fetch(`${API_URL}/${id}`);
  if (!res.ok) throw new Error("Erro ao buscar tarefa");

  return res.json();
}

export async function createTask(
  task: Omit<Task, "id" | "createdAt" | "completedAt">,
): Promise<void> {
  const res = await fetch(API_URL, {
    method: "POST",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify(task),
  });
  if (!res.ok) throw new Error("Erro ao criar tarefa");
}

export async function updateTask(
  id: number,
  updates: Partial<Task>,
): Promise<void> {
  const res = await fetch(`${API_URL}/${id}`, {
    method: "PATCH",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify(updates),
  });
  if (!res.ok) throw new Error("Erro ao atualizar tarefa");
}

export async function deleteTask(id: number): Promise<void> {
  const res = await fetch(`${API_URL}/${id}`, {
    method: "DELETE",
  });
  if (!res.ok) throw new Error("Erro ao excluir tarefa");
}

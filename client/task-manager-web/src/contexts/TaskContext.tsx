"use client";
import React, { createContext, useContext, useState, useEffect } from "react";
import { Task } from "@/types/task";
import {
  getAllTasks,
  createTask,
  updateTask,
  deleteTask,
} from "@/services/taskService";

interface TaskContextProps {
  tasks: Task[];
  fetchTasks: () => Promise<void>;
  addTask: (
    task: Omit<Task, "id" | "createdAt" | "completedAt">,
  ) => Promise<void>;
  updateTask: (id: number, updates: Partial<Task>) => Promise<void>;
  deleteTask: (id: number) => Promise<void>;
}

const TaskContext = createContext<TaskContextProps | undefined>(undefined);

export const TaskProvider: React.FC<{ children: React.ReactNode }> = ({
  children,
}) => {
  const [tasks, setTasks] = useState<Task[]>([]);

  const fetchTasks = async () => {
    const data = await getAllTasks();
    setTasks(data);
  };

  const addTask = async (
    taskData: Omit<Task, "id" | "createdAt" | "completedAt">,
  ) => {
    await createTask(taskData);
    await fetchTasks();
  };

  const updateTaskFn = async (id: number, updates: Partial<Task>) => {
    await updateTask(id, updates);
    await fetchTasks();
  };

  const deleteTaskFn = async (id: number) => {
    await deleteTask(id);
    await fetchTasks();
  };

  useEffect(() => {
    fetchTasks();
  }, []);

  return (
    <TaskContext.Provider
      value={{
        tasks,
        fetchTasks,
        addTask,
        updateTask: updateTaskFn,
        deleteTask: deleteTaskFn,
      }}
    >
      {children}
    </TaskContext.Provider>
  );
};

export const useTaskContext = () => {
  const context = useContext(TaskContext);
  if (!context)
    throw new Error("useTaskContext must be used within a TaskProvider");
  return context;
};

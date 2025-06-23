"use client";
import React, { createContext, useContext, useState, useCallback } from "react";
import {
  Task,
  CreateTaskRequest,
  UpdateTaskRequest,
  TaskStatus,
} from "@/types/task";
import { TaskService } from "@/services/taskService";

interface TaskContextType {
  tasks: Task[];
  loading: boolean;
  error: string | null;
  totalCount: number;
  currentPage: number;
  pageSize: number;
  filters: {
    title: string;
    status: TaskStatus | null;
  };
  fetchTasks: () => Promise<void>;
  createTask: (task: CreateTaskRequest) => Promise<void>;
  updateTask: (id: number, task: UpdateTaskRequest) => Promise<void>;
  deleteTask: (id: number) => Promise<void>;
  setFilters: (filters: { title: string; status: TaskStatus | null }) => void;
  setPage: (page: number) => void;
}

const TaskContext = createContext<TaskContextType | undefined>(undefined);

export const useTaskContext = () => {
  const context = useContext(TaskContext);
  if (!context) {
    throw new Error("useTaskContext must be used within a TaskProvider");
  }
  return context;
};

export const TaskProvider: React.FC<{ children: React.ReactNode }> = ({
  children,
}) => {
  const [tasks, setTasks] = useState<Task[]>([]);
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState<string | null>(null);
  const [totalCount, setTotalCount] = useState(0);
  const [currentPage, setCurrentPage] = useState(1);
  const [pageSize] = useState(12);
  const [filters, setFiltersState] = useState<{
    title: string;
    status: TaskStatus | null;
  }>({
    title: "",
    status: null,
  });

  const fetchTasks = useCallback(async () => {
    setLoading(true);
    setError(null);

    try {
      const response = await TaskService.getAllTasks(
        filters.title || undefined,
        filters.status || undefined,
        currentPage,
        pageSize,
      );

      setTasks(response.data);
      setTotalCount(response.count);
    } catch (err) {
      setError(err instanceof Error ? err.message : "An error occurred");
    } finally {
      setLoading(false);
    }
  }, [filters, currentPage, pageSize]);

  const createTask = async (task: CreateTaskRequest) => {
    try {
      await TaskService.createTask(task);
      await fetchTasks();
    } catch (err) {
      setError(err instanceof Error ? err.message : "Failed to create task");
      throw err;
    }
  };

  const updateTask = async (id: number, task: UpdateTaskRequest) => {
    try {
      await TaskService.updateTask(id, task);
      await fetchTasks();
    } catch (err) {
      setError(err instanceof Error ? err.message : "Failed to update task");
      throw err;
    }
  };

  const deleteTask = async (id: number) => {
    try {
      await TaskService.deleteTask(id);
      await fetchTasks();
    } catch (err) {
      setError(err instanceof Error ? err.message : "Failed to delete task");
      throw err;
    }
  };

  const setFilters = (newFilters: {
    title: string;
    status: TaskStatus | null;
  }) => {
    setFiltersState(newFilters);
    setCurrentPage(1);
  };

  const setPage = (page: number) => {
    setCurrentPage(page);
  };

  return (
    <TaskContext.Provider
      value={{
        tasks,
        loading,
        error,
        totalCount,
        currentPage,
        pageSize,
        filters,
        fetchTasks,
        createTask,
        updateTask,
        deleteTask,
        setFilters,
        setPage,
      }}
    >
      {children}
    </TaskContext.Provider>
  );
};

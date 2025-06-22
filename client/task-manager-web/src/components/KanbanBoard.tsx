"use client";
import React, { useState } from "react";
import { useTaskContext } from "@/contexts/TaskContext";
import { KanbanColumn } from "./KanbanColumn";
import { Task } from "@/types/task";
import { labels } from "@/utils/labels";

const statusLabels = {
  1: labels.pending,
  2: labels.inProgress,
  3: labels.completed,
};

export const KanbanBoard: React.FC = () => {
  const { tasks, addTask, updateTask } = useTaskContext();
  const [editingTaskId, setEditingTaskId] = useState<number | null>(null);
  const [editTitle, setEditTitle] = useState("");
  const [editDescription, setEditDescription] = useState("");

  const handleAddTask = async (title: string, description: string) => {
    const createdById = tasks[0]?.createdBy || "default-user";
    await addTask({
      title,
      description,
      status: 1,
      createdBy: createdById,
    });
  };

  const startEditTask = (task: Task) => {
    setEditingTaskId(task.id);
    setEditTitle(task.title);
    setEditDescription(task.description);
  };

  const handleEditTaskSave = async (task: Task) => {
    await updateTask(task.id, {
      title: editTitle,
      description: editDescription,
    });
    setEditingTaskId(null);
    setEditTitle("");
    setEditDescription("");
  };

  const handleEditTaskCancel = () => {
    setEditingTaskId(null);
    setEditTitle("");
    setEditDescription("");
  };

  const handleDropTask = async (taskId: number, newStatus: 1 | 2 | 3) => {
    const task = tasks.find((t) => t.id === taskId);
    if (task && task.status !== newStatus) {
      await updateTask(taskId, { status: newStatus });
    }
  };

  return (
    <div className="flex gap-4 overflow-x-auto py-4">
      {[1, 2, 3].map((status) => (
        <KanbanColumn
          key={status}
          title={statusLabels[status as 1 | 2 | 3]}
          status={status as 1 | 2 | 3}
          tasks={tasks.filter((t) => t.status === status)}
          onEdit={startEditTask}
          onDropTask={(taskId) => handleDropTask(taskId, status as 1 | 2 | 3)}
          onAddTask={status === 1 ? handleAddTask : undefined}
          onDragOver={(e) => e.preventDefault()}
          editingTaskId={editingTaskId}
          editTitle={editTitle}
          setEditTitle={setEditTitle}
          editDescription={editDescription}
          setEditDescription={setEditDescription}
          onEditSave={handleEditTaskSave}
          onEditCancel={handleEditTaskCancel}
        />
      ))}
    </div>
  );
};

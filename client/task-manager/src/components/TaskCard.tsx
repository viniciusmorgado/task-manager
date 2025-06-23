"use client";

import React, { useState } from "react";
import { Task, TaskStatus } from "@/types/task";
import { getStatusLabel, getStatusColor } from "@/utils/labels";
import { useTaskContext } from "@/context/TaskContext";
import { Edit, Trash2, Calendar } from "lucide-react";

interface TaskCardProps {
  task: Task;
}

export const TaskCard: React.FC<TaskCardProps> = ({ task }) => {
  const { updateTask, deleteTask } = useTaskContext();
  const [isEditing, setIsEditing] = useState(false);
  const [editForm, setEditForm] = useState({
    title: task.title,
    description: task.description,
    status: task.status,
  });

  const handleStatusChange = async (newStatus: TaskStatus) => {
    try {
      await updateTask(task.id, {
        ...task,
        status: newStatus,
        createdById: task.createdById,
      });
    } catch (error) {
      console.error("Failed to update task status:", error);
    }
  };

  const handleEdit = async () => {
    try {
      await updateTask(task.id, {
        ...editForm,
        createdById: task.createdById,
      });
      setIsEditing(false);
    } catch (error) {
      console.error("Failed to update task:", error);
    }
  };

  const handleDelete = async () => {
    if (window.confirm("Tem certeza que deseja excluir esta tarefa?")) {
      try {
        await deleteTask(task.id);
      } catch (error) {
        console.error("Failed to delete task:", error);
      }
    }
  };

  if (isEditing) {
    return (
      <div className="bg-white rounded-lg shadow-md p-4 border border-gray-200">
        <input
          type="text"
          value={editForm.title}
          onChange={(e) => setEditForm({ ...editForm, title: e.target.value })}
          className="w-full mb-2 p-2 border rounded"
          placeholder="Título da tarefa"
        />
        <textarea
          value={editForm.description}
          onChange={(e) =>
            setEditForm({ ...editForm, description: e.target.value })
          }
          className="w-full mb-2 p-2 border rounded resize-none"
          rows={3}
          placeholder="Descrição da tarefa"
        />
        <select
          value={editForm.status}
          onChange={(e) =>
            setEditForm({
              ...editForm,
              status: Number(e.target.value) as TaskStatus,
            })
          }
          className="w-full mb-3 p-2 border rounded"
        >
          <option value={TaskStatus.TODO}>A Fazer</option>
          <option value={TaskStatus.IN_PROGRESS}>Em Progresso</option>
          <option value={TaskStatus.COMPLETED}>Concluído</option>
        </select>
        <div className="flex gap-2">
          <button
            onClick={handleEdit}
            className="flex-1 bg-blue-500 text-white px-3 py-1 rounded hover:bg-blue-600"
          >
            Salvar
          </button>
          <button
            onClick={() => setIsEditing(false)}
            className="flex-1 bg-gray-500 text-white px-3 py-1 rounded hover:bg-gray-600"
          >
            Cancelar
          </button>
        </div>
      </div>
    );
  }

  return (
    <div className="bg-white rounded-lg shadow-md p-4 border border-gray-200 hover:shadow-lg transition-shadow">
      <div className="flex justify-between items-start mb-2">
        <h3 className="font-semibold text-gray-800 line-clamp-2">
          {task.title}
        </h3>
        <div className="flex gap-1 ml-2">
          <button
            onClick={() => setIsEditing(true)}
            className="p-1 text-gray-500 hover:text-blue-500"
          >
            <Edit size={16} />
          </button>
          <button
            onClick={handleDelete}
            className="p-1 text-gray-500 hover:text-red-500"
          >
            <Trash2 size={16} />
          </button>
        </div>
      </div>

      <p className="text-gray-600 text-sm mb-3 line-clamp-3">
        {task.description}
      </p>

      <div className="flex items-center justify-between">
        <span
          className={`px-2 py-1 rounded-full text-xs font-medium border ${getStatusColor(task.status)}`}
        >
          {getStatusLabel(task.status)}
        </span>

        <div className="flex items-center text-xs text-gray-500">
          <Calendar size={12} className="mr-1" />
          {new Date(task.createdAt).toLocaleDateString("pt-BR")}
        </div>
      </div>

      <div className="mt-3 flex gap-1">
        {task.status !== TaskStatus.TODO && (
          <button
            onClick={() => handleStatusChange(TaskStatus.TODO)}
            className="text-xs px-2 py-1 bg-orange-100 hover:bg-orange-200 text-orange-800 rounded"
          >
            → A Fazer
          </button>
        )}
        {task.status !== TaskStatus.IN_PROGRESS && (
          <button
            onClick={() => handleStatusChange(TaskStatus.IN_PROGRESS)}
            className="text-xs px-2 py-1 bg-blue-100 hover:bg-blue-200 rounded"
          >
            → Em Progresso
          </button>
        )}
        {task.status !== TaskStatus.COMPLETED && (
          <button
            onClick={() => handleStatusChange(TaskStatus.COMPLETED)}
            className="text-xs px-2 py-1 bg-green-100 hover:bg-green-200 rounded"
          >
            → Concluído
          </button>
        )}
      </div>
    </div>
  );
};

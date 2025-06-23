"use client";

import React from "react";
import { Task, TaskStatus } from "@/types/task";
import { TaskCard } from "./TaskCard";
import { getStatusLabel } from "@/utils/labels";

interface KanbanBoardProps {
  tasks: Task[];
}

export const KanbanBoard: React.FC<KanbanBoardProps> = ({ tasks }) => {
  const getTasksByStatus = (status: TaskStatus) => {
    return tasks.filter((task) => task.status === status);
  };

  const getColumnColor = (status: TaskStatus) => {
    switch (status) {
      case TaskStatus.TODO:
        return "border-orange-300 bg-orange-50";
      case TaskStatus.IN_PROGRESS:
        return "border-blue-300 bg-blue-50";
      case TaskStatus.COMPLETED:
        return "border-green-300 bg-green-50";
      default:
        return "border-gray-300 bg-gray-50";
    }
  };

  return (
    <div className="grid grid-cols-1 md:grid-cols-3 gap-6">
      {[TaskStatus.TODO, TaskStatus.IN_PROGRESS, TaskStatus.COMPLETED].map(
        (status) => {
          const columnTasks = getTasksByStatus(status);

          return (
            <div
              key={status}
              className={`border-2 rounded-lg p-4 ${getColumnColor(status)}`}
            >
              <div className="flex items-center justify-between mb-4">
                <h2 className="font-bold text-lg text-gray-800">
                  {getStatusLabel(status)}
                </h2>
                <span className="bg-white px-2 py-1 rounded-full text-sm font-medium text-gray-600">
                  {columnTasks.length}
                </span>
              </div>

              <div className="space-y-3">
                {columnTasks.map((task) => (
                  <TaskCard key={task.id} task={task} />
                ))}

                {columnTasks.length === 0 && (
                  <div className="text-center py-8 text-gray-500">
                    <p>Nenhuma tarefa nesta coluna</p>
                  </div>
                )}
              </div>
            </div>
          );
        },
      )}
    </div>
  );
};

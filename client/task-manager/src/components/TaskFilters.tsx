"use client";

import React from "react";
import { TaskStatus } from "@/types/task";
import { useTaskContext } from "@/context/TaskContext";
import { Search, Filter } from "lucide-react";

export const TaskFilters: React.FC = () => {
  const { filters, setFilters } = useTaskContext();

  const handleTitleChange = (title: string) => {
    setFilters({ ...filters, title });
  };

  const handleStatusChange = (status: string) => {
    const statusValue = status === "" ? null : (Number(status) as TaskStatus);
    setFilters({ ...filters, status: statusValue });
  };

  return (
    <div className="bg-white rounded-lg shadow-md p-4 mb-6">
      <div className="flex items-center gap-4 flex-wrap">
        <div className="flex items-center gap-2 flex-1 min-w-64">
          <Search size={20} className="text-gray-500" />
          <input
            type="text"
            placeholder="Buscar por título..."
            value={filters.title}
            onChange={(e) => handleTitleChange(e.target.value)}
            className="flex-1 p-2 border border-gray-300 rounded-lg focus:outline-none focus:ring-2 focus:ring-blue-500"
          />
        </div>

        <div className="flex items-center gap-2">
          <Filter size={20} className="text-gray-500" />
          <select
            value={filters.status || ""}
            onChange={(e) => handleStatusChange(e.target.value)}
            className="p-2 border border-gray-300 rounded-lg focus:outline-none focus:ring-2 focus:ring-blue-500"
          >
            <option value="">Todos os status</option>
            <option value={TaskStatus.TODO}>A Fazer</option>
            <option value={TaskStatus.IN_PROGRESS}>Em Progresso</option>
            <option value={TaskStatus.COMPLETED}>Concluído</option>
          </select>
        </div>
      </div>
    </div>
  );
};

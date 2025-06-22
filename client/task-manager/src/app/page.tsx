"use client";

import React, { useState, useEffect } from "react";
import { TaskProvider, useTaskContext } from "@/context/TaskContext";
import { KanbanBoard } from "@/components/KanbanBoard";
import { TaskFilters } from "@/components/TaskFilters";
import { CreateTaskModal } from "@/components/CreateTaskModal";
import { Pagination } from "@/components/Pagination";
import { Plus, RefreshCw } from "lucide-react";

const TaskManagerContent: React.FC = () => {
  const { tasks, loading, error, fetchTasks } = useTaskContext();
  const [isCreateModalOpen, setIsCreateModalOpen] = useState(false);

  useEffect(() => {
    fetchTasks();
  }, [fetchTasks]);

  if (error) {
    return (
      <div className="min-h-screen bg-gray-100 flex items-center justify-center">
        <div className="bg-white p-6 rounded-lg shadow-md">
          <h2 className="text-xl font-bold text-red-600 mb-2">Erro</h2>
          <p className="text-gray-700">{error}</p>
          <button
            onClick={fetchTasks}
            className="mt-4 px-4 py-2 bg-blue-500 text-white rounded hover:bg-blue-600"
          >
            Tentar Novamente
          </button>
        </div>
      </div>
    );
  }

  return (
    <div className="min-h-screen bg-gray-100">
      <div className="container mx-auto px-4 py-8">
        <div className="flex justify-between items-center mb-8">
          <h1 className="text-3xl font-bold text-gray-800">
            Gerenciador de Tarefas
          </h1>
          <div className="flex gap-3">
            <button
              onClick={fetchTasks}
              disabled={loading}
              className="flex items-center gap-2 px-4 py-2 border border-gray-300 text-gray-700 rounded-lg hover:bg-gray-50 disabled:opacity-50"
            >
              <RefreshCw size={20} className={loading ? "animate-spin" : ""} />
              Atualizar
            </button>
            <button
              onClick={() => setIsCreateModalOpen(true)}
              className="flex items-center gap-2 px-4 py-2 bg-blue-500 text-white rounded-lg hover:bg-blue-600"
            >
              <Plus size={20} />
              Nova Tarefa
            </button>
          </div>
        </div>

        <TaskFilters />

        {loading ? (
          <div className="flex justify-center items-center py-12">
            <div className="animate-spin rounded-full h-12 w-12 border-b-2 border-blue-500"></div>
          </div>
        ) : (
          <>
            <KanbanBoard tasks={tasks} />
            <Pagination />
          </>
        )}

        <CreateTaskModal
          isOpen={isCreateModalOpen}
          onClose={() => setIsCreateModalOpen(false)}
        />
      </div>
    </div>
  );
};

export default function Home() {
  return (
    <TaskProvider>
      <TaskManagerContent />
    </TaskProvider>
  );
}

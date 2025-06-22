"use client";
import React, { useState, useMemo } from "react";
import { useTaskContext } from "@/contexts/TaskContext";
import { Task } from "@/types/task";
import { labels } from "@/utils/labels";
import { Button } from "./Button";
import { Input } from "./Input";
import { Modal } from "./Modal";
import { Badge } from "./Badge";

type ViewMode = "list" | "grid";
type SortBy = "createdAt" | "title" | "status";
type FilterBy = "all" | 1 | 2 | 3;

export const TaskListView: React.FC = () => {
  const { tasks, updateTask, deleteTask } = useTaskContext();
  const [viewMode, setViewMode] = useState<ViewMode>("list");
  const [sortBy, setSortBy] = useState<SortBy>("createdAt");
  const [filterBy, setFilterBy] = useState<FilterBy>("all");
  const [searchTerm, setSearchTerm] = useState("");
  const [selectedTask, setSelectedTask] = useState<Task | null>(null);
  const [taskToDelete, setTaskToDelete] = useState<Task | null>(null);

  // Função para formatar data
  const formatDate = (dateString: string) => {
    return new Date(dateString).toLocaleDateString("pt-BR", {
      day: "2-digit",
      month: "2-digit",
      year: "numeric",
      hour: "2-digit",
      minute: "2-digit",
    });
  };

  // Função para obter o nome do status
  const getStatusName = (status: number) => {
    switch (status) {
      case 1:
        return labels.pending;
      case 2:
        return labels.inProgress;
      case 3:
        return labels.completed;
      default:
        return "Desconhecido";
    }
  };

  // Função para obter a cor do badge
  const getStatusColor = (status: number) => {
    switch (status) {
      case 1:
        return "yellow";
      case 2:
        return "blue";
      case 3:
        return "green";
      default:
        return "gray";
    }
  };

  // Filtrar e ordenar tarefas
  const filteredAndSortedTasks = useMemo(() => {
    let filtered = tasks;

    // Filtrar por status
    if (filterBy !== "all") {
      filtered = filtered.filter((task) => task.status === filterBy);
    }

    // Filtrar por termo de busca
    if (searchTerm) {
      filtered = filtered.filter(
        (task) =>
          task.title.toLowerCase().includes(searchTerm.toLowerCase()) ||
          task.description.toLowerCase().includes(searchTerm.toLowerCase()),
      );
    }

    // Ordenar
    filtered.sort((a, b) => {
      switch (sortBy) {
        case "title":
          return a.title.localeCompare(b.title);
        case "status":
          return a.status - b.status;
        case "createdAt":
        default:
          return (
            new Date(b.createdAt).getTime() - new Date(a.createdAt).getTime()
          );
      }
    });

    return filtered;
  }, [tasks, filterBy, searchTerm, sortBy]);

  // Função para alterar status da tarefa
  const handleStatusChange = async (task: Task, newStatus: 1 | 2 | 3) => {
    const updates: Partial<Task> = { status: newStatus };

    if (newStatus === 3) {
      updates.completedAt = new Date().toISOString();
    } else if (task.status === 3 && newStatus !== 3) {
      updates.completedAt = null;
    }

    await updateTask(task.id, updates);
  };

  // Função para confirmar exclusão
  const handleDeleteConfirm = async () => {
    if (taskToDelete) {
      await deleteTask(taskToDelete.id);
      setTaskToDelete(null);
    }
  };

  return (
    <div className="space-y-6">
      {/* Header com controles */}
      <div className="flex flex-col lg:flex-row gap-4 items-start lg:items-center justify-between">
        <h2 className="text-2xl font-bold text-gray-800">{labels.taskList}</h2>

        <div className="flex flex-wrap gap-3 items-center">
          {/* Busca */}
          <Input
            placeholder="Buscar tarefas..."
            value={searchTerm}
            onChange={(e) => setSearchTerm(e.target.value)}
            className="w-64"
          />

          {/* Filtro por status */}
          <select
            value={filterBy}
            onChange={(e) => setFilterBy(e.target.value as FilterBy)}
            className="px-3 py-2 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-blue-500"
          >
            <option value="all">Todos os status</option>
            <option value={1}>{labels.pending}</option>
            <option value={2}>{labels.inProgress}</option>
            <option value={3}>{labels.completed}</option>
          </select>

          {/* Ordenação */}
          <select
            value={sortBy}
            onChange={(e) => setSortBy(e.target.value as SortBy)}
            className="px-3 py-2 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-blue-500"
          >
            <option value="createdAt">Data de criação</option>
            <option value="title">Título</option>
            <option value="status">Status</option>
          </select>

          {/* Toggle de visualização */}
          <div className="flex bg-gray-100 rounded-lg p-1">
            <button
              onClick={() => setViewMode("list")}
              className={`px-3 py-2 rounded-md text-sm font-medium transition-colors ${
                viewMode === "list"
                  ? "bg-white text-blue-600 shadow-sm"
                  : "text-gray-600 hover:text-gray-800"
              }`}
            >
              📋 Lista
            </button>
            <button
              onClick={() => setViewMode("grid")}
              className={`px-3 py-2 rounded-md text-sm font-medium transition-colors ${
                viewMode === "grid"
                  ? "bg-white text-blue-600 shadow-sm"
                  : "text-gray-600 hover:text-gray-800"
              }`}
            >
              ⊞ Grade
            </button>
          </div>
        </div>
      </div>

      {/* Contador de tarefas */}
      <div className="text-sm text-gray-600">
        Mostrando {filteredAndSortedTasks.length} de {tasks.length} tarefas
      </div>

      {/* Lista/Grade de tarefas */}
      {filteredAndSortedTasks.length === 0 ? (
        <div className="text-center py-12">
          <div className="text-gray-400 text-6xl mb-4">📝</div>
          <h3 className="text-lg font-medium text-gray-600 mb-2">
            Nenhuma tarefa encontrada
          </h3>
          <p className="text-gray-500">
            {searchTerm || filterBy !== "all"
              ? "Tente ajustar os filtros de busca"
              : "Adicione uma nova tarefa no quadro Kanban"}
          </p>
        </div>
      ) : viewMode === "list" ? (
        /* Visualização em Lista */
        <div className="bg-white rounded-lg border border-gray-200 overflow-hidden">
          <div className="overflow-x-auto">
            <table className="w-full">
              <thead className="bg-gray-50">
                <tr>
                  <th className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                    Tarefa
                  </th>
                  <th className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                    Status
                  </th>
                  <th className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                    Criado em
                  </th>
                  <th className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                    Concluído em
                  </th>
                  <th className="px-6 py-3 text-right text-xs font-medium text-gray-500 uppercase tracking-wider">
                    Ações
                  </th>
                </tr>
              </thead>
              <tbody className="bg-white divide-y divide-gray-200">
                {filteredAndSortedTasks.map((task) => (
                  <tr key={task.id} className="hover:bg-gray-50">
                    <td className="px-6 py-4">
                      <div
                        className="cursor-pointer"
                        onClick={() => setSelectedTask(task)}
                      >
                        <div className="text-sm font-medium text-gray-900">
                          {task.title}
                        </div>
                        {task.description && (
                          <div className="text-sm text-gray-500 mt-1 line-clamp-2">
                            {task.description}
                          </div>
                        )}
                      </div>
                    </td>
                    <td className="px-6 py-4 whitespace-nowrap">
                      <select
                        value={task.status}
                        onChange={(e) =>
                          handleStatusChange(
                            task,
                            Number(e.target.value) as 1 | 2 | 3,
                          )
                        }
                        className="text-sm border-none bg-transparent focus:outline-none"
                      >
                        <option value={1}>{labels.pending}</option>
                        <option value={2}>{labels.inProgress}</option>
                        <option value={3}>{labels.completed}</option>
                      </select>
                    </td>
                    <td className="px-6 py-4 whitespace-nowrap text-sm text-gray-500">
                      {formatDate(task.createdAt)}
                    </td>
                    <td className="px-6 py-4 whitespace-nowrap text-sm text-gray-500">
                      {task.completedAt ? formatDate(task.completedAt) : "-"}
                    </td>
                    <td className="px-6 py-4 whitespace-nowrap text-right text-sm font-medium">
                      <div className="flex gap-2 justify-end">
                        <Button
                          onClick={() => setSelectedTask(task)}
                          className="text-blue-600 hover:text-blue-900 bg-transparent hover:bg-blue-50 px-3 py-1 text-sm"
                        >
                          Ver
                        </Button>
                        <Button
                          onClick={() => setTaskToDelete(task)}
                          className="text-red-600 hover:text-red-900 bg-transparent hover:bg-red-50 px-3 py-1 text-sm"
                        >
                          Excluir
                        </Button>
                      </div>
                    </td>
                  </tr>
                ))}
              </tbody>
            </table>
          </div>
        </div>
      ) : (
        /* Visualização em Grade */
        <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 xl:grid-cols-4 gap-4">
          {filteredAndSortedTasks.map((task) => (
            <div
              key={task.id}
              className="bg-white rounded-lg border border-gray-200 p-4 hover:shadow-md transition-shadow cursor-pointer"
              onClick={() => setSelectedTask(task)}
            >
              <div className="flex justify-between items-start mb-3">
                <h3 className="font-semibold text-gray-800 text-sm leading-tight flex-1 pr-2">
                  {task.title}
                </h3>
                <Badge color={getStatusColor(task.status)}>
                  {getStatusName(task.status)}
                </Badge>
              </div>

              {task.description && (
                <p className="text-gray-600 text-sm mb-3 line-clamp-3">
                  {task.description}
                </p>
              )}

              <div className="border-t border-gray-100 pt-3 space-y-1">
                <div className="text-xs text-gray-500">
                  Criado: {formatDate(task.createdAt)}
                </div>
                {task.completedAt && (
                  <div className="text-xs text-green-600">
                    Concluído: {formatDate(task.completedAt)}
                  </div>
                )}
              </div>

              <div className="flex gap-2 mt-3 pt-3 border-t border-gray-100">
                <select
                  value={task.status}
                  onChange={(e) => {
                    e.stopPropagation();
                    handleStatusChange(
                      task,
                      Number(e.target.value) as 1 | 2 | 3,
                    );
                  }}
                  className="flex-1 text-xs px-2 py-1 border border-gray-300 rounded focus:outline-none focus:ring-1 focus:ring-blue-500"
                >
                  <option value={1}>{labels.pending}</option>
                  <option value={2}>{labels.inProgress}</option>
                  <option value={3}>{labels.completed}</option>
                </select>
                <Button
                  onClick={(e) => {
                    e.stopPropagation();
                    setTaskToDelete(task);
                  }}
                  className="text-red-600 hover:text-red-900 bg-transparent hover:bg-red-50 px-2 py-1 text-xs"
                >
                  🗑️
                </Button>
              </div>
            </div>
          ))}
        </div>
      )}

      {/* Modal de detalhes da tarefa */}
      {selectedTask && (
        <Modal
          isOpen={!!selectedTask}
          onClose={() => setSelectedTask(null)}
          title="Detalhes da Tarefa"
        >
          <div className="space-y-4">
            <div>
              <label className="block text-sm font-medium text-gray-700 mb-1">
                Título
              </label>
              <p className="text-gray-900 font-semibold">
                {selectedTask.title}
              </p>
            </div>

            {selectedTask.description && (
              <div>
                <label className="block text-sm font-medium text-gray-700 mb-1">
                  Descrição
                </label>
                <p className="text-gray-700 whitespace-pre-wrap">
                  {selectedTask.description}
                </p>
              </div>
            )}

            <div>
              <label className="block text-sm font-medium text-gray-700 mb-1">
                Status
              </label>
              <Badge color={getStatusColor(selectedTask.status)}>
                {getStatusName(selectedTask.status)}
              </Badge>
            </div>

            <div className="grid grid-cols-1 md:grid-cols-2 gap-4">
              <div>
                <label className="block text-sm font-medium text-gray-700 mb-1">
                  Criado em
                </label>
                <p className="text-gray-600">
                  {formatDate(selectedTask.createdAt)}
                </p>
              </div>

              {selectedTask.completedAt && (
                <div>
                  <label className="block text-sm font-medium text-gray-700 mb-1">
                    Concluído em
                  </label>
                  <p className="text-gray-600">
                    {formatDate(selectedTask.completedAt)}
                  </p>
                </div>
              )}
            </div>

            <div className="flex gap-3 pt-4 border-t border-gray-200">
              <select
                value={selectedTask.status}
                onChange={(e) =>
                  handleStatusChange(
                    selectedTask,
                    Number(e.target.value) as 1 | 2 | 3,
                  )
                }
                className="flex-1 px-3 py-2 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-blue-500"
              >
                <option value={1}>{labels.pending}</option>
                <option value={2}>{labels.inProgress}</option>
                <option value={3}>{labels.completed}</option>
              </select>

              <Button
                onClick={() => setTaskToDelete(selectedTask)}
                className="bg-red-500 hover:bg-red-600 text-white px-4 py-2"
              >
                {labels.deleteTask}
              </Button>
            </div>
          </div>
        </Modal>
      )}

      {/* Modal de confirmação de exclusão */}
      {taskToDelete && (
        <Modal
          isOpen={!!taskToDelete}
          onClose={() => setTaskToDelete(null)}
          title="Confirmar Exclusão"
        >
          <div className="space-y-4">
            <p className="text-gray-700">{labels.confirmDelete}</p>
            <p className="font-semibold text-gray-900">
              "{taskToDelete.title}"
            </p>

            <div className="flex gap-3 pt-4">
              <Button
                onClick={() => setTaskToDelete(null)}
                className="flex-1 bg-gray-500 hover:bg-gray-600 text-white"
              >
                Cancelar
              </Button>
              <Button
                onClick={handleDeleteConfirm}
                className="flex-1 bg-red-500 hover:bg-red-600 text-white"
              >
                Excluir
              </Button>
            </div>
          </div>
        </Modal>
      )}
    </div>
  );
};

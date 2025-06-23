import { TaskStatus } from "@/types/task";

export const getStatusLabel = (status: TaskStatus): string => {
  switch (status) {
    case TaskStatus.TODO:
      return "A Fazer";
    case TaskStatus.IN_PROGRESS:
      return "Em Progresso";
    case TaskStatus.COMPLETED:
      return "Concluído";
    default:
      return "Desconhecido";
  }
};

export const getStatusColor = (status: TaskStatus): string => {
  switch (status) {
    case TaskStatus.TODO:
      return "bg-orange-100 text-orange-800 border-orange-200";
    case TaskStatus.IN_PROGRESS:
      return "bg-blue-100 text-blue-800 border-blue-200";
    case TaskStatus.COMPLETED:
      return "bg-green-100 text-green-800 border-green-200";
    default:
      return "bg-gray-100 text-gray-800 border-gray-200";
  }
};

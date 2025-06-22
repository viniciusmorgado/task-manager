import React from "react";

interface BadgeProps {
  status: 1 | 2 | 3;
}

const statusMap = {
  1: { label: "Pendente", color: "bg-yellow-400 text-yellow-900" },
  2: { label: "Em Progresso", color: "bg-blue-400 text-blue-900" },
  3: { label: "Concluída", color: "bg-green-400 text-green-900" },
};

export const Badge: React.FC<BadgeProps> = ({ status }) => {
  const { label, color } = statusMap[status];
  return (
    <span className={`px-2 py-1 rounded text-xs font-semibold ${color}`}>
      {label}
    </span>
  );
};

"use client";
import React from "react";
import { useTaskContext } from "@/context/TaskContext";
import { ChevronLeft, ChevronRight } from "lucide-react";

export const Pagination: React.FC = () => {
  const { currentPage, pageSize, totalCount, setPage } = useTaskContext();
  const totalPages = Math.ceil(totalCount / pageSize);

  if (totalPages <= 1) return null;

  const getPageNumbers = () => {
    const pages = [];
    const maxVisible = 5;

    let start = Math.max(1, currentPage - Math.floor(maxVisible / 2));
    const end = Math.min(totalPages, start + maxVisible - 1);

    if (end - start + 1 < maxVisible) {
      start = Math.max(1, end - maxVisible + 1);
    }

    for (let i = start; i <= end; i++) {
      pages.push(i);
    }

    return pages;
  };

  return (
    <div className="flex items-center justify-center gap-2 mt-6">
      <button
        onClick={() => setPage(currentPage - 1)}
        disabled={currentPage === 1}
        className="p-2 border border-gray-300 rounded-lg hover:bg-gray-50 disabled:opacity-50 disabled:cursor-not-allowed"
      >
        <ChevronLeft size={20} />
      </button>

      {getPageNumbers().map((page) => (
        <button
          key={page}
          onClick={() => setPage(page)}
          className={`px-3 py-2 border rounded-lg ${
            page === currentPage
              ? "bg-blue-500 text-white border-blue-500"
              : "border-gray-300 hover:bg-gray-50"
          }`}
        >
          {page}
        </button>
      ))}

      <button
        onClick={() => setPage(currentPage + 1)}
        disabled={currentPage === totalPages}
        className="p-2 border border-gray-300 rounded-lg hover:bg-gray-50 disabled:opacity-50 disabled:cursor-not-allowed"
      >
        <ChevronRight size={20} />
      </button>

      <span className="ml-4 text-sm text-gray-600">
        Página {currentPage} de {totalPages} ({totalCount} tarefas)
      </span>
    </div>
  );
};

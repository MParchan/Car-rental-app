import React from "react";

export default function Pagination({
  pageNumber,
  totalPages,
  pageSize,
  setPageNumber,
  setPageSize
}: {
  pageNumber: number;
  totalPages: number;
  pageSize: number;
  setPageNumber: (page: number) => void;
  setPageSize: (page: number) => void;
}) {
  return (
    <div className="flex justify-end p-4 text-lg items-center">
      <div
        className="cursor-pointer"
        onClick={() => (pageNumber - 1 >= 1 ? setPageNumber(pageNumber - 1) : setPageNumber(1))}
      >
        <img src="/assets/icons/arrow_left.svg" className="h-7" alt="Car Rental Logo" />
      </div>
      <div className="mx-1">
        <span className="font-semibold">{pageNumber}</span>
        <span> of </span>
        <span className="font-semibold">{totalPages}</span>
      </div>
      <div
        className="cursor-pointer"
        onClick={() =>
          pageNumber + 1 <= totalPages ? setPageNumber(pageNumber + 1) : setPageNumber(totalPages)
        }
      >
        <img src="/assets/icons/arrow_right.svg" className="h-7" alt="Car Rental Logo" />
      </div>
      <select
        className="w-14 p-1 rounded-lg bg-gray-50 border border-gray-300 text-gray-900 focus:border-gray-900 focus:border-2 hover:cursor-pointer"
        value={pageSize}
        onChange={(e) => {
          setPageSize(Number(e.target.value));
          setPageNumber(1);
        }}
      >
        <option value={5}>5</option>
        <option value={10}>10</option>
        <option value={25}>25</option>
      </select>
    </div>
  );
}

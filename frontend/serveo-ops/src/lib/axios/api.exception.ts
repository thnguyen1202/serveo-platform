import type { ApiProblemDetails } from './api.types';

export class ApiException extends Error {
  readonly problem: ApiProblemDetails;
  readonly status?: number;

  constructor(problem: ApiProblemDetails, status?: number) {
    super(problem.detail ?? problem.title ?? 'Unexpected error');

    this.problem = problem;
    this.status = status;
    this.name = problem.title ?? 'ApiException';
  }
}
